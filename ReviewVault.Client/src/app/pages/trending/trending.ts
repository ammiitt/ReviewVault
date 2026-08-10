import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { MediaTypeResponse } from '../../core/models/category.model';
import { PostResponse } from '../../core/models/post.model';
import { MediaTypeService } from '../../core/services/media-type';
import { PostService } from '../../core/services/post';
import { PostCard } from '../../shared/post-card/post-card';
import { FormsModule } from '@angular/forms';
import { JikanItem } from '../../core/models/jikan.model';
import { TmdbItem } from '../../core/models/tmdb.model';
import { JikanService } from '../../core/services/jikan';
import { TmdbService } from '../../core/services/tmdb';

@Component({
  selector: 'app-trending',
  imports: [CommonModule, PostCard, FormsModule],
  templateUrl: './trending.html',
  styleUrl: './trending.css',
})
export class Trending implements OnInit {

    selectedSection = 'reviews';
    loading = false;
    jikanError = false;

    // Your reviews
    reviewPosts: PostResponse[] = [];

    // External API data
    tmdbItems: TmdbItem[] = [];
    jikanItems: JikanItem[] = [];

    constructor(
        private postService: PostService,
        private tmdbService: TmdbService,
        private jikanService: JikanService
    ) { }

    ngOnInit(): void {
        this.loadReviews();
    }

    onSectionChange(): void {
        this.tmdbItems = [];
        this.jikanItems = [];

        switch (this.selectedSection) {
            case 'reviews':
                this.loadReviews();
                break;
            case 'movies':
                this.loadMovies();
                break;
            case 'tvshows':
                this.loadTVShows();
                break;
            case 'kdrama':
                this.loadKDramas();
                break;
            case 'anime':
                this.loadAnime();
                break;
            case 'manga':
                this.loadManga();
                break;
        }
    }

    // ═══ YOUR REVIEWS ═══
    loadReviews(): void {
        this.loading = true;
        this.postService.getAllPublished(1, 20).subscribe({
            next: (response) => {
                this.reviewPosts = response.data.sort((a, b) => b.rating - a.rating);
                this.loading = false;
            },
            error: () => this.loading = false
        });
    }

    // ═══ TMDB ═══
    loadMovies(): void {
        this.loading = true;
        this.tmdbService.getTrendingMovies().subscribe({
            next: (response) => {
                this.tmdbItems = response.results;
                this.loading = false;
            },
            error: () => this.loading = false
        });
    }

    loadTVShows(): void {
        this.loading = true;
        this.tmdbService.getTrendingTV().subscribe({
            next: (response) => {
                this.tmdbItems = response.results;
                this.loading = false;
            },
            error: () => this.loading = false
        });
    }

    loadKDramas(): void {
        this.loading = true;
        this.tmdbService.getKDramas().subscribe({
            next: (response) => {
                this.tmdbItems = response.results;
                this.loading = false;
            },
            error: () => this.loading = false
        });
    }

    // ═══ JIKAN ═══
    loadAnime(): void {
    this.loading = true;
    this.jikanError = false;
    this.jikanService.getTopAnime(20).subscribe({
        next: (response) => {
            this.jikanItems = response.data;
            this.jikanError = response.data.length === 0;
            this.loading = false;
        },
        error: () => {
            this.jikanError = true;
            this.loading = false;
        }
    });
}

   loadManga(): void {
    this.loading = true;
    this.jikanError = false;
    this.jikanService.getTopManga(20).subscribe({
        next: (response) => {
            this.jikanItems = response.data;
            this.jikanError = response.data.length === 0;
            this.loading = false;
        },
        error: () => {
            this.jikanError = true;
            this.loading = false;
        }
    });
    }

    // TMDB image URL builder
    getImageUrl(path: string | null): string {
        return this.tmdbService.getImageUrl(path);
    }
}
