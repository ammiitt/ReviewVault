import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { ReactiveFormsModule, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { RouterModule, Router } from '@angular/router';
import { CategoryResponse, MediaTypeResponse } from '../../../core/models/category.model';
import { CategoryService } from '../../../core/services/category';
import { MediaTypeService } from '../../../core/services/media-type';
import { PostService } from '../../../core/services/post';

@Component({
  selector: 'app-create-post',
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './create-post.html',
  styleUrl: './create-post.css',
})
export class CreatePost {
    postForm: FormGroup;
    categories: CategoryResponse[] = [];
    mediaTypes: MediaTypeResponse[] = [];
    selectedCategoryIds: number[] = [];
    loading = false;
    submitted = false;
    error = '';
    success = false;
    createdSlug = '';

    constructor(
        private fb: FormBuilder,
        private postService: PostService,
        private categoryService: CategoryService,
        private mediaTypeService: MediaTypeService,
        private router: Router
    ) {
        this.postForm = this.fb.group({
            title: ['', [Validators.required, Validators.maxLength(200)]],
            body: ['', [Validators.required, Validators.minLength(50)]],
            summary: [''],
            coverImageUrl: [''],
            rating: [0, [Validators.min(1), Validators.max(5)]],
            mediaTypeId: [0, [Validators.min(1)]],
            isPublished: [false]
        });
    }

    ngOnInit(): void {
        // Load categories and media types for dropdowns
        this.categoryService.getAll().subscribe({
            next: (cats) => this.categories = cats
        });

        this.mediaTypeService.getAll().subscribe({
            next: (types) => this.mediaTypes = types
        });
    }

    // Shortcut for form controls
    get f() { return this.postForm.controls; }

    // Handle category checkbox toggle
    onCategoryChange(event: Event, id: number): void {
        const checked = (event.target as HTMLInputElement).checked;
        if (checked) {
            this.selectedCategoryIds.push(id);
        } else {
            this.selectedCategoryIds = this.selectedCategoryIds.filter(c => c !== id);
        }
    }

    // Handle broken image preview
    onImageError(event: Event): void {
        (event.target as HTMLImageElement).style.display = 'none';
    }

    onSubmit(): void {
        this.submitted = true;
        this.error = '';
        this.success = false;

        // Check categories separately (not in FormGroup)
        if (this.postForm.invalid || this.selectedCategoryIds.length === 0) return;

        this.loading = true;

        const request = {
            ...this.postForm.value,
            rating: Number(this.postForm.value.rating),
            mediaTypeId: Number(this.postForm.value.mediaTypeId),
            categoryIds: this.selectedCategoryIds
        };

        this.postService.create(request).subscribe({
            next: (post) => {
                this.success = true;
                this.createdSlug = post.slug;
                this.loading = false;
                // Reset form
                this.postForm.reset({ isPublished: false, rating: 0, mediaTypeId: 0 });
                this.selectedCategoryIds = [];
                this.submitted = false;
            },
            error: (err) => {
                this.error = err.message;
                this.loading = false;
            }
        });
    }
}
