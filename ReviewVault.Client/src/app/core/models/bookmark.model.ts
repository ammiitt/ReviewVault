export interface BookmarkResponse {
    id: number;
    postId: number;
    postTitle: string;
    postSlug: string;
    postCoverImageUrl: string | null;
    createdAt: string;
}