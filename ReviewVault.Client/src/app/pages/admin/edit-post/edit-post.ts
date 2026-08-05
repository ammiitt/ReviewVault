import { CommonModule } from '@angular/common';
import id from '@angular/common/locales/id';
import { Component } from '@angular/core';
import { ReactiveFormsModule, FormsModule, Validators, FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink, RouterModule } from '@angular/router';
import { MediaTypeService } from '../../../core/services/media-type';
import { CategoryService } from '../../../core/services/category';
import { PostService } from '../../../core/services/post';
import { CategoryResponse, MediaTypeResponse } from '../../../core/models/category.model';

@Component({
  selector: 'app-edit-post',
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './edit-post.html',
  styleUrl: './edit-post.css',
})
export class EditPost  {
    postForm!: FormGroup;
    categories: CategoryResponse[] = [];
    mediaTypes: MediaTypeResponse[] = [];
    selectedCategoryIds: number[] = [];
    postId = 0;
    currentSlug = '';
    updatedSlug = '';
    pageLoading = true;
    loading = false;
    submitted = false;
    error = '';
    success = false;

    constructor(
        private fb: FormBuilder,
        private route: ActivatedRoute,      // reads :id from URL
        private router: Router,
        private postService: PostService,
        private categoryService: CategoryService,
        private mediaTypeService: MediaTypeService
    ) { }

    ngOnInit(): void {
    // Build the form structure
        this.postForm = this.fb.group({
            title: ['', [Validators.required, Validators.maxLength(200)]],
            body: ['', [Validators.required, Validators.minLength(50)]],
            summary: [''],
            coverImageUrl: [''],
            rating: [0, [Validators.min(1), Validators.max(5)]],
            mediaTypeId: [0, [Validators.min(1)]],
            isPublished: [false]
        });

        // Load categories and media types
        this.categoryService.getAll().subscribe({
            next: (cats) => this.categories = cats
        });

        this.mediaTypeService.getAll().subscribe({
            next: (types) => this.mediaTypes = types
        });

        // Get post ID from URL and load the post
        this.route.params.subscribe(params => {
            this.postId = Number(params['id']);
            this.loadPost();
        });
    }

    get f() { return this.postForm.controls; }

    loadPost(): void {
    this.pageLoading = true;

    this.postService.getById(this.postId).subscribe({
        next: (post) => {
            // Fill form with existing data
            this.postForm.patchValue({
                title: post.title,
                body: post.body,
                summary: post.summary,
                coverImageUrl: post.coverImageUrl,
                rating: post.rating,
                mediaTypeId: post.mediaTypeId,
                isPublished: post.isPublished
            });

            this.currentSlug = post.slug;

            // Now we have categoryIds directly — no name matching needed!
            this.selectedCategoryIds = post.categoryIds || [];

            this.pageLoading = false;
        },
        error: (err) => {
            this.error = err.message;
            this.pageLoading = false;
        }
    });
}

    // Check if a category checkbox should be checked
    isCategorySelected(id: number): boolean {
        return this.selectedCategoryIds.includes(id);
    }

    onCategoryChange(event: Event, id: number): void {
        const checked = (event.target as HTMLInputElement).checked;
        if (checked) {
            this.selectedCategoryIds.push(id);
        } else {
            this.selectedCategoryIds = this.selectedCategoryIds.filter(c => c !== id);
        }
    }

    onImageError(event: Event): void {
        (event.target as HTMLImageElement).style.display = 'none';
    }

    onSubmit(): void {
        this.submitted = true;
        this.error = '';
        this.success = false;

        if (this.postForm.invalid || this.selectedCategoryIds.length === 0) return;

        this.loading = true;

        const request = {
            ...this.postForm.value,
            rating: Number(this.postForm.value.rating),
            mediaTypeId: Number(this.postForm.value.mediaTypeId),
            categoryIds: this.selectedCategoryIds
        };

        this.postService.update(this.postId, request).subscribe({
            next: (post) => {
                this.success = true;
                this.updatedSlug = post.slug;
                this.currentSlug = post.slug;
                this.loading = false;
                this.submitted = false;
            },
            error: (err) => {
                this.error = err.message;
                this.loading = false;
            }
        });
    }
}
