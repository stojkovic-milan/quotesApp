import { FC } from 'react';
import { SortType } from '../../utils/enums';


type SortInputProps = {
    sortType: SortType,
    setSortType: React.Dispatch<React.SetStateAction<SortType>>
}
const sortOptions: { display: string, value: string }[] = [
    { display: "Default", value: "Default" },
    { display: "Rating Ascending", value: "RatingAsc", },
    { display: "Rating Descending", value: "RatingDesc", },
    { display: "Ratings Number Ascending", value: "RatingNumAsc", },
    { display: "Ratings Number Descending", value: "RatingNumDesc", },
    { display: "Author", value: "Author", },
    { display: "Content", value: "Content", },
]
const SortInput: FC<SortInputProps> = ({ sortType, setSortType }) => {
    const handleSortChange = (selectedSortType: string) => {
        //@ts-ignore
        setSortType(SortType[Object.keys(SortType).find(x => x == selectedSortType)])
    }
    return (
        <div className='h-8 w-full flex flex-row max-h-16 items-center justify-center gap-1 text-xl text-gray-800'>
            <select value={sortType} onChange={(ev) => handleSortChange(ev.target.value)}>
                {sortOptions.map((opt) =>
                    <option key={opt.value} value={opt.value}>
                        {opt.display}
                    </option>
                )}
            </select>
        </div>
    )
}
export default SortInput;