export interface JikanResponse {
    data: JikanItem[];
    pagination: {
        last_visible_page: number;
        has_next_page: boolean;
    };
}

export interface JikanItem {
    mal_id: number;
    title: string;
    title_english: string | null;
    images: {
        jpg: {
            image_url: string;
            large_image_url: string;
        };
    };
    score: number | null;
    synopsis: string | null;
    episodes: number | null;
    chapters: number | null;
    type: string;
    status: string;
    aired?: {
        string: string;
    };
    published?: {
        string: string;
    };
}