import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { PostResponse } from '../../../core/models/post.model';
import { PostService } from '../../../core/services/post';

@Component({
  selector: 'app-dashboard',
  imports: [CommonModule, RouterModule],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css',
})
export class Dashboard {
    posts: PostResponse[] = [];
    loading = false;
    filter: 'all' | 'published' | 'draft' = 'all';

    constructor(private postService: PostService) { }

    ngOnInit(): void {
        this.loadPosts();
    }

    loadPosts(): void {
        this.loading = true;
        this.postService.getAllPublished(1, 50).subscribe({
            next: (response) => {
                this.posts = response.data;
                this.loading = false;
            },
            error: () => {
                this.loading = false;
            }
        });
    }

    getFilteredPosts(): PostResponse[] {
        if (this.filter === 'published') return this.posts.filter(p => p.isPublished);
        if (this.filter === 'draft') return this.posts.filter(p => !p.isPublished);
        return this.posts;
    }

    getPublishedCount(): number {
        return this.posts.filter(p => p.isPublished).length;
    }

    getDraftCount(): number {
        return this.posts.filter(p => !p.isPublished).length;
    }

    getRatingClass(rating: number): string {
        switch (rating) {
            case 5: return 'bg-success';
            case 4: return 'bg-primary';
            case 3: return 'bg-warning';
            case 2: return 'bg-orange';
            case 1: return 'bg-danger';
            default: return 'bg-secondary';
        }
    }

    deletePost(id: number, title: string): void {
        if (confirm(`Delete "${title}"? This cannot be undone.`)) {
            this.postService.remove(id).subscribe({
                next: () => {
                    // Remove from local array without reloading
                    this.posts = this.posts.filter(p => p.id !== id);
                },
                error: (err) => {
                    alert('Failed to delete: ' + err.message);
                }
            });
        }
    }
}
