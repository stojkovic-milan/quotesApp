import { Quote } from "./Quote";

export interface QuoteDisplayDTO {
    quote: Quote;
    percentage: number;
    userVotePositive: boolean | null;
}
export interface RatingDTO {
    quoteId: string;
    positive: boolean;
}

export interface QuotesResponseDTO {
    quotes: QuoteDisplayDTO[];
    totalCount: number;
    totalPages: number;
    currentPage: number;
}