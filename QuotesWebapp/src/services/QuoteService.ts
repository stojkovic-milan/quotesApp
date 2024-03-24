import { QuotesResponseDTO } from "../models/DTOs";
import { SortType } from "../utils/enums";
import { axiosInstance } from "../utils/http";

export const QuoteService = {
    getQuotes: (pageNum = 1, sortType?: SortType, filterTags?: string[]) => {
        console.log(filterTags)
        let query = `page=${pageNum}`;
        if (sortType)
            query += `&sortType=${sortType}`;
        if (filterTags && filterTags.length > 0)
            filterTags.forEach(t => query = query.concat(`&filterTags=${t}`))
        return axiosInstance.get<QuotesResponseDTO>(`/quote?${query}`)
    },

    getAllTags: () => {
        return axiosInstance.get<string[]>(`/quote/tags`)
    }
};