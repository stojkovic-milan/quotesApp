import { FC } from 'react';
import { SortType } from '../../utils/enums';


type TagFilterInputProps = {
    tagOptions: string[],
    filteredTags: string[],
    setFilteredTags: React.Dispatch<React.SetStateAction<string[]>>
}

const TagFilterInput: FC<TagFilterInputProps> = ({ tagOptions, setFilteredTags, filteredTags }) => {
    const handleFilterChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        var updatedTags = [...filteredTags];
        if (event.target.checked) {
            updatedTags = [...filteredTags, event.target.value];
        } else {
            updatedTags.splice(filteredTags.indexOf(event.target.value), 1);
        }
        setFilteredTags(updatedTags);
    }

    return (
        <div className='h-12 w-full flex flex-col max-h-20 items-center justify-center text-xl overflow-y-auto overflow-x-hidden pt-6'>
            {tagOptions.map((tag, index) => (
                <div key={index}>
                    <input value={tag} type="checkbox" checked={filteredTags.includes(tag)}
                        onChange={(ev) => handleFilterChange(ev)} />
                    <span>{tag}</span>
                </div>
            ))}
        </div>
    )
}
export default TagFilterInput;