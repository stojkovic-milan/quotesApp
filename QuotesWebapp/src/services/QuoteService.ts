import { QuotesResponseDTO } from "../models/DTOs";
import { SortType } from "../utils/enums";
import { axiosInstance } from "../utils/http";

export const QuoteService = {
    getQuotes: (pageNum = 1, sortType?: SortType) => {
        return axiosInstance.get<QuotesResponseDTO>(`/quote?page=${pageNum}${sortType ? `&sortType=${sortType}` : ''}`)
    }
};
