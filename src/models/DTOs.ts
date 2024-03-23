import { Quote } from "./Quote";
import { Rating } from "./Rating";

export interface QuoteGetDTO {
    quote: Quote;
    positiveCount: number;
    negativeCount: number;
    percentage: number;
    userVotePositive: boolean | null;
}
export interface RatingDTO {
    quoteId: string;
    positive: boolean;
}

export interface QuotesResponseDTO {
    quotes: QuoteGetDTO[];
    totalCount: number;
    totalPages: number;
    currentPage: number;
}