import { FC, useEffect, useState } from "react"
import QuoteDisplay from "../componenets/quoteDisplay/QuoteDisplay"
import { QuoteService } from "../services/QuoteService"
import axios from "axios"
import { QuoteDisplayDTO, RatingDTO } from "../models/DTOs"
import { RatingService } from "../services/RatingService"
import { Pagination } from "../models/Pagination"
import PaginationInput from "../componenets/paginationInput/PaginationInput"
import { SortType } from "../utils/enums"
import SortInput from "../componenets/sortInput/SortInput"
import TagFilterInput from "../componenets/tagFilterInput/TagFilterInput"

const Quotes: FC = () => {
    const [quotes, setQuotes] = useState<Array<QuoteDisplayDTO>>([])
    const [pagination, setPagination] = useState<Pagination>()
    const [sortType, setSortType] = useState<SortType>(SortType.RatingDesc)
    const [tagOptions, setTagOptions] = useState<string[]>([])
    const [filteredTags, setFilteredTags] = useState<string[]>([])
    //TODO: ??Add QuotesContainer Componenet, to hold logic and display quotes??
    const handleQuoteRate = async (rating: RatingDTO) => {
        const { data: response, status } = await RatingService.rateQuote(rating);
        setQuotes(prev =>
            prev.map(q => {
                if (q.quote.id == rating.quoteId)
                    return { ...response }
                else
                    return q
            })
        )
    }
    const fetchQuotes = async () => {
        try {
            const { data: response, status } = await QuoteService.getQuotes(pagination?.currentPage, sortType, filteredTags);
            setQuotes(response.quotes);
            // if (!pagination)
            setPagination({ ...response });
        } catch (error) {
            if (axios.isAxiosError(error)) console.error(error.message);
        }
    };
    const fetchTags = async () => {
        try {
            const { data: response, status } = await QuoteService.getAllTags();
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
        <div className="bg-gray-600 h-full w-full font-roboto">
            <div className='w-full flex justify-center items-center pt-4 text-white flex-col gap-2'>
                <h1 className='text-5xl'>Quotes</h1>
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
                            <TagFilterInput setFilteredTags={setFilteredTags} tagOptions={tagOptions} filteredTags={filteredTags} />
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
export default Quotes;