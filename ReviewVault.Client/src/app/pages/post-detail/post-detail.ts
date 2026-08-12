import { Component } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { PostResponse } from '../../core/models/post.model';
import { PostService } from '../../core/services/post';
import { CommonModule } from '@angular/common';
import { ToastService } from '../../core/services/toast';

@Component({
  selector: 'app-post-detail',
  imports: [CommonModule, RouterModule],
  templateUrl: './post-detail.html',
  styleUrl: './post-detail.css',
})
export class PostDetail {
    post: PostResponse | null = null;
    loading = false;
    error = '';

    constructor(
        private route: ActivatedRoute,    // reads URL parameters
        private router: Router,
        private postService: PostService,
        private toastr: ToastService
    ) { }

    ngOnInit(): void {
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
            },
            error: (err) => {
                this.error = err.message;
                this.loading = false;
            }
        });
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
