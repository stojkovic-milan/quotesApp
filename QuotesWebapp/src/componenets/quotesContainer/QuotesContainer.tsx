import axios from "axios"
import { FC, useState, useEffect } from "react"
import { QuoteDisplayDTO, RatingDTO } from "../../models/DTOs"
import { Pagination } from "../../models/Pagination"
import { QuoteService } from "../../services/QuoteService"
import { RatingService } from "../../services/RatingService"
import { SortType } from "../../utils/enums"
import PaginationInput from "../paginationInput/PaginationInput"
import QuoteDisplay from "../quoteDisplay/QuoteDisplay"
import SortInput from "../sortInput/SortInput"
import TagFilterInput from "../tagFilterInput/TagFilterInput"


const QuotesContainer: FC = () => {
    const [quotes, setQuotes] = useState<Array<QuoteDisplayDTO>>([])
    const [pagination, setPagination] = useState<Pagination>()
    const [sortType, setSortType] = useState<SortType>(SortType.Default)
    const [tagOptions, setTagOptions] = useState<string[]>([])
    const [filteredTags, setFilteredTags] = useState<string[]>([])

    const handleQuoteRate = async (rating: RatingDTO) => {
        try {
            const { data: response } = await RatingService.rateQuote(rating);
            setQuotes(prev =>
                prev.map(q => {
                    if (q.quote.id == rating.quoteId)
                        return { ...response }
                    else
                        return q
                })
            )
        } catch (error) {
            if (axios.isAxiosError(error)) console.error(error.message);
        }

    }
    const fetchQuotes = async () => {
        try {
            const { data: response } = await QuoteService.getQuotes(pagination?.currentPage, sortType, filteredTags);
            setQuotes(response.quotes);
            setPagination({ ...response });
        } catch (error) {
            if (axios.isAxiosError(error)) console.error(error.message);
        }
    };
    const fetchTags = async () => {
        try {
            const { data: response } = await QuoteService.getAllTags();
            setTagOptions(response);
        } catch (error) {
            if (axios.isAxiosError(error)) console.error(error.message);
        }
    };
    useEffect(() => {
        fetchQuotes();
        fetchTags();
    }, [])

    useEffect(() => {
        pagination && fetchQuotes();
    }, [pagination?.currentPage, sortType, filteredTags])

    return (
        <div className="h-full w-full">
            <div className='w-full flex justify-center items-center pt-4 text-white flex-col gap-2'>
                <div className="flex flex-row w-full justify-center gap-4 items-center pt-4">
                    <div className="flex gap-2">
                        <span className="text-xl">Order by:</span>
                        <div>
                            <SortInput setSortType={setSortType} sortType={sortType} />
                        </div>
                    </div>
                    {tagOptions && <div className="flex gap-2 items-center">
                        <span className="text-xl">Filter Tags:</span>
                        <div>
                            <TagFilterInput setFilteredTags={setFilteredTags} tagOptions={tagOptions}
                                filteredTags={filteredTags} setPagination={setPagination} />
                        </div>
                    </div>}
                </div>
            </div>
            <div className='w-full flex flex-col items-center pt-2 px-12 gap-6'>
                {quotes && quotes.length > 0 &&
                    quotes.map((q, ind) =>
                        <QuoteDisplay key={ind} quoteDto={q} handleQuoteRate={handleQuoteRate} />
                    )}
            </div>
            <div className="pt-4 pb-4">
                {pagination && <PaginationInput pagination={pagination} setPagination={setPagination} />}
            </div>
        </div>)
}
export default QuotesContainer;