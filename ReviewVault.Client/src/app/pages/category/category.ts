import { CommonModule } from '@angular/common';
import { PostCard } from '../../shared/post-card/post-card';
import { Component } from '@angular/core';
import { RouterModule, ActivatedRoute } from '@angular/router';
import { CategoryResponse } from '../../core/models/category.model';
import { PostResponse } from '../../core/models/post.model';
import { CategoryService } from '../../core/services/category';
import { PostService } from '../../core/services/post';


@Component({
  selector: 'app-category',
  imports: [CommonModule, RouterModule,PostCard],
  templateUrl: './category.html',
  styleUrl: './category.css',
})
export class Category {
    categoryName = '';
    posts: PostResponse[] = [];
    allCategories: CategoryResponse[] = [];
    loading = false;
    error = '';

    constructor(
        private route: ActivatedRoute,
        private postService: PostService,
        private categoryService: CategoryService
    ) { }

    ngOnInit(): void {
        // Load all categories for the filter pills
        this.categoryService.getAll().subscribe({
            next: (cats) => this.allCategories = cats
        });

        // Listen to route changes — when user clicks different category pill
        // This re-runs without reloading the component
        this.route.params.subscribe(params => {
            this.categoryName = params['name'];
            this.loadPosts();
        });
    }

    loadPosts(): void {
        this.loading = true;
        this.error = '';
        this.posts = [];

        // First find the category ID from the name
        this.categoryService.getAll().subscribe({
            next: (cats) => {
                const category = cats.find(
                    c => c.name.toLowerCase() === this.categoryName.toLowerCase()
                );

                if (!category) {
                    this.error = `Category "${this.categoryName}" not found`;
                    this.loading = false;
                    return;
                }

                // Now load posts for this category
                this.postService.getByCategory(category.id).subscribe({
                    next: (posts) => {
                        this.posts = posts;
                        this.loading = false;
                    },
                    error: (err) => {
                        this.error = err.message;
                        this.loading = false;
                    }
                });
            },
            error: (err) => {
                this.error = err.message;
                this.loading = false;
            }
        });
    }
}
