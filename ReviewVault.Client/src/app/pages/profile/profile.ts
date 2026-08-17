import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ReactiveFormsModule, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { BookmarkResponse } from '../../core/models/bookmark.model';
import { UserProfile } from '../../core/models/user.model';
import { BookmarkService } from '../../core/services/bookmark';
import { ToastService } from '../../core/services/toast';
import { UserService } from '../../core/services/user';

@Component({
  selector: 'app-profile',
  imports: [CommonModule, RouterModule, ReactiveFormsModule],
  templateUrl: './profile.html',
  styleUrl: './profile.css',
})
export class Profile implements OnInit {
    profile: UserProfile | null = null;
    bookmarks: BookmarkResponse[] = [];
    passwordForm: FormGroup;
    loading = true;
    passwordLoading = false;

    constructor(
        private fb: FormBuilder,
        private userService: UserService,
        private bookmarkService: BookmarkService,
        private toast: ToastService
    ) {
        this.passwordForm = this.fb.group({
            currentPassword: ['', Validators.required],
            newPassword: ['', [Validators.required, Validators.minLength(6)]]
        });
    }

    ngOnInit(): void {
        this.loadProfile();
        this.loadBookmarks();
    }

    loadProfile(): void {
        this.userService.getProfile().subscribe({
            next: (profile) => {
                this.profile = profile;
                this.loading = false;
            },
            error: () => this.loading = false
        });
    }

    loadBookmarks(): void {
        this.bookmarkService.getMyBookmarks().subscribe({
            next: (bookmarks) => this.bookmarks = bookmarks
        });
    }

    changePassword(): void {
        if (this.passwordForm.invalid) return;

        this.passwordLoading = true;
        this.userService.changePassword(this.passwordForm.value).subscribe({
            next: () => {
                this.toast.success('Password changed!', 'Success 🔐');
                this.passwordForm.reset();
                this.passwordLoading = false;
            },
            error: (err) => {
                this.toast.error(err.message, 'Failed');
                this.passwordLoading = false;
            }
        });
    }
}
