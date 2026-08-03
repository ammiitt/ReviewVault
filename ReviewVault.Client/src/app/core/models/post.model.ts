export interface PostResponse {
    id: number;
    title: string;
    slug: string;
    body: string;
    summary: string | null;
    coverImageUrl: string | null;
    rating: number;
    ratingName: string;
    mediaTypeName: string;
    authorName: string;
    isPublished: boolean;
    publishedAt: string | null;
    createdAt: string;
    categories: string[];
}

export interface PostListResponse {
    data: PostResponse[];
    totalCount: number;
    page: number;
    pageSize: number;
    totalPages: number;
}

export interface CreatePostRequest {
    title: string;
    body: string;
    summary?: string;
    coverImageUrl?: string;
    rating: number;
    mediaTypeId: number;
    categoryIds: number[];
    isPublished: boolean;
}

export interface UpdatePostRequest {
    title: string;
    body: string;
    summary?: string;
    coverImageUrl?: string;
    rating: number;
    mediaTypeId: number;
    categoryIds: number[];
    isPublished: boolean;
}