import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { PostResponse } from '../../core/models/post.model';
import { PostService } from '../../core/services/post';
import { Pagination } from '../../shared/pagination/pagination';
import { PostCard } from '../../shared/post-card/post-card';

@Component({
  selector: 'app-search',
  imports: [CommonModule, FormsModule,PostCard,Pagination],
  templateUrl: './search.html',
  styleUrl: './search.css',
})
export class Search {
  query = '';
    searchedQuery = '';
    posts: PostResponse[] = [];
    loading = false;
    searched = false;
    currentPage = 1;
    totalPages = 0;
    totalCount = 0;

    constructor(
        private postService: PostService,
        private route: ActivatedRoute,
        private router: Router
    ) {
        // Check if query param exists in URL
        this.route.queryParams.subscribe(params => {
            if (params['q']) {
                this.query = params['q'];
                this.search();
            }
        });
    }

    search(): void {
        if (!this.query.trim()) return;

        this.searchedQuery = this.query.trim();
        this.currentPage = 1;

        // Update URL with query param
        this.router.navigate([], {
            queryParams: { q: this.searchedQuery },
            queryParamsHandling: 'merge'
        });

        this.loadPage(1);
    }

    loadPage(page: number): void {
        this.loading = true;
        this.currentPage = page;

        this.postService.search(this.searchedQuery, page, 8).subscribe({
            next: (response) => {
                this.posts = response.data;
                this.totalPages = response.totalPages;
                this.totalCount = response.totalCount;
                this.searched = true;
                this.loading = false;
            },
            error: () => {
                this.loading = false;
                this.searched = true;
            }
        });
    }
}
