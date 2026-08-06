import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { MediaTypeResponse } from '../../core/models/category.model';
import { PostResponse } from '../../core/models/post.model';
import { MediaTypeService } from '../../core/services/media-type';
import { PostService } from '../../core/services/post';
import { PostCard } from '../../shared/post-card/post-card';

@Component({
  selector: 'app-trending',
  imports: [CommonModule, PostCard],
  templateUrl: './trending.html',
  styleUrl: './trending.css',
})
export class Trending implements OnInit {

    allPosts: PostResponse[] = [];
    mediaTypes: MediaTypeResponse[] = [];
    selectedType = 'all';
    loading = false;

    constructor(
        private postService: PostService,
        private mediaTypeService: MediaTypeService
    ) { }

    ngOnInit(): void {
        this.loading = true;

        // Load media types for filter buttons
        this.mediaTypeService.getAll().subscribe({
            next: (types) => this.mediaTypes = types
        });

        // Load all posts and sort by rating (highest first)
        this.postService.getAllPublished(1, 50).subscribe({
            next: (response) => {
                // Sort by rating descending, then by date
                this.allPosts = response.data.sort((a, b) => {
                    if (b.rating !== a.rating) return b.rating - a.rating;
                    return new Date(b.publishedAt || '').getTime() -
                           new Date(a.publishedAt || '').getTime();
                });
                this.loading = false;
            },
            error: () => {
                this.loading = false;
            }
        });
    }

    filterByType(type: string): void {
        this.selectedType = type;
    }

    getFilteredPosts(): PostResponse[] {
        if (this.selectedType === 'all') return this.allPosts;
        return this.allPosts.filter(p => p.mediaTypeName === this.selectedType);
    }
}
