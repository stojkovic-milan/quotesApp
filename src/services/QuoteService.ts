import { QuotesResponseDTO } from "../models/DTOs";
import { axiosInstance } from "../utils/http";

export const QuoteService = {
    getQuotes: (pageNum = 1) => {
        return axiosInstance.get<QuotesResponseDTO>(`/quote?page=${pageNum}`)
    }
};
