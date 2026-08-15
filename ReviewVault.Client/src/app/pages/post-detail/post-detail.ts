import { Component } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { PostResponse } from '../../core/models/post.model';
import { PostService } from '../../core/services/post';
import { CommonModule } from '@angular/common';
import { ToastService } from '../../core/services/toast';
import { FormsModule } from '@angular/forms';
import { CommentResponse } from '../../core/models/comment.model';
import { CommentService } from '../../core/services/comment';
import { AuthService } from '../../core/services/auth';

@Component({
  selector: 'app-post-detail',
  imports: [CommonModule, RouterModule, FormsModule],
  templateUrl: './post-detail.html',
  styleUrl: './post-detail.css',
})
export class PostDetail {

    post: PostResponse | null = null;
    comments: CommentResponse[] = [];
    newComment = '';
    loading = false;
    commentLoading = false;
    error = '';
    isLoggedIn: any;
    currentUserId = 0;
    isAdmin = false;


    constructor(
        private route: ActivatedRoute,    // reads URL parameters
        private router: Router,
        private postService: PostService,
        private toastr: ToastService,
        private commentService: CommentService,
        private authService: AuthService
    ) { }

    ngOnInit(): void {

        // Check auth state
        this.authService.currentUser$.subscribe(user => {
            this.isLoggedIn = user !== null;
            this.isAdmin = user?.role === 'Admin';
        });

        // Get current user ID from token
        const token = this.authService.getAccessToken();
        if (token) {
            const payload = JSON.parse(atob(token.split('.')[1]));
            this.currentUserId = parseInt(payload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'] || '0');
        }
        // Get the slug from URL: /post/attack-on-titan-review
        // ActivatedRoute gives us access to :slug parameter
        this.route.params.subscribe(params => {
            const slug = params['slug'];
            this.loadPost(slug);
        });
    }

    loadPost(slug: string): void {
        this.loading = true;
        this.error = '';

        this.postService.getBySlug(slug).subscribe({
            next: (post) => {
                this.post = post;
                this.loading = false;

                // Update browser tab title
                document.title = `${post.title} | ReviewVault`;
                this.loadComments(post.id);
            },
            error: (err) => {
                this.error = err.message;
                this.loading = false;
            }
        });
    }


    loadComments(postId: number): void {
        this.commentService.getByPostId(postId).subscribe({
            next: (comments) => this.comments = comments
        });
    }

    submitComment(): void {
        if (!this.newComment.trim() || !this.post) return;

        this.commentLoading = true;

        this.commentService.create({
            body: this.newComment.trim(),
            postId: this.post.id
        }).subscribe({
            next: (comment) => {
                this.comments.unshift(comment);   // add to top
                this.newComment = '';
                this.commentLoading = false;
                this.toastr.success('Comment posted!', 'Success 💬');
            },
            error: (err) => {
                this.toastr.error(err.message, 'Failed to post');
                this.commentLoading = false;
            }
        });
    }

    deleteComment(id: number): void {
        if (!confirm('Delete this comment?')) return;

        this.commentService.remove(id).subscribe({
            next: () => {
                this.comments = this.comments.filter(c => c.id !== id);
                this.toastr.success('Comment deleted', 'Removed 🗑️');
            },
            error: (err) => {
                this.toastr.error(err.message, 'Delete failed');
            }
        });
    }

    canDeleteComment(comment: CommentResponse): boolean {
        return this.isAdmin || comment.userId === this.currentUserId;
    }
    // Calculate reading time: avg person reads 200 words/min
    getReadingTime(): number {
        if (!this.post) return 0;
        const words = this.post.body.split(' ').length;
        return Math.max(1, Math.ceil(words / 200));
    }

    // Rating badge color
    getRatingClass(): string {
        switch (this.post?.rating) {
            case 5: return 'bg-success';
            case 4: return 'bg-primary';
            case 3: return 'bg-warning';
            case 2: return 'bg-orange';
            case 1: return 'bg-danger';
            default: return 'bg-secondary';
        }
    }

    // Navigate to category page
    goToCategory(name: string): void {
        this.router.navigate(['/category', name]);
    }

    // Share using browser's native share or copy URL
    sharePost(): void {
    const url = window.location.href;

    if (navigator.share) {
        navigator.share({
            title: this.post?.title,
            text: this.post?.summary || '',
            url: url
        });
    } else {
        navigator.clipboard.writeText(url).then(() => {
            this.toastr.success('Link copied to clipboard!', 'Share 📋');
        });
    }
}
}
