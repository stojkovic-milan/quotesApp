import { FC } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faCaretLeft, faCaretRight } from '@fortawesome/free-solid-svg-icons';
import { Pagination } from '../../models/Pagination';

type PaginationInputProps = {
    pagination: Pagination,
    setPagination: React.Dispatch<React.SetStateAction<Pagination | undefined>>
}
const PaginationInput: FC<PaginationInputProps> = ({ pagination, setPagination }) => {
    const handlePageChange = (offset: number) => {
        let newPageNum = offset + pagination.currentPage
        if (1 <= newPageNum && newPageNum <= pagination.totalPages)
            setPagination(prevPagination => ({ ...(prevPagination as Pagination), currentPage: newPageNum }))
    }
    return (
        <div className='h-full w-full flex flex-row max-h-16 items-center justify-center gap-1 text-xl text-white'>
            <FontAwesomeIcon icon={faCaretLeft} size='xl'
                className='cursor-pointer'
                color={pagination.currentPage == 1 ? 'gray' : 'white'}
                onClick={() => handlePageChange(-1)}
            />
            <span>{`${pagination.currentPage} / ${pagination.totalPages}`}</span>
            <FontAwesomeIcon icon={faCaretRight} size='xl'
                className='cursor-pointer'
                color={pagination.currentPage == pagination.totalPages ? 'gray' : 'white'}
                onClick={() => handlePageChange(1)}
            />
        </div>
    )
}
export default PaginationInput;