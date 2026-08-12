import { Component, OnInit } from '@angular/core';
import { PostResponse } from '../../core/models/post.model';
import { PostService } from '../../core/services/post';
import { CommonModule } from '@angular/common';
import { PostCard } from '../../shared/post-card/post-card';
import { RouterModule } from '@angular/router';
import { Pagination } from '../../shared/pagination/pagination';

@Component({
  selector: 'app-home',
  imports: [CommonModule, PostCard, RouterModule, Pagination ],
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class Home implements OnInit {

    posts: PostResponse[] = [];
    loading = false;
    error = '';
    currentPage = 1;
    pageSize = 4;
    totalPages = 1;
    totalCount = 0;

    constructor(private postService: PostService) { }

    // OnInit — runs when component loads (like Page_Load in .NET)
    ngOnInit(): void {
        this.loadPage(1);
    }

    loadPage(page: number): void {
        this.loading = true;
        this.error = '';
        this.currentPage = page;

        this.postService.getAllPublished(page, this.pageSize).subscribe({
            // Success — data received
            next: (response) => {
                this.posts = response.data;
                this.totalPages = response.totalPages;
                this.totalCount = response.totalCount;
                this.loading = false;

                window.scrollTo({ top: 0, behavior: 'smooth' });
            },
            // Error — something went wrong
            error: (err) => {
                this.error = err.message;
                this.loading = false;
            }
        });
    }
}

