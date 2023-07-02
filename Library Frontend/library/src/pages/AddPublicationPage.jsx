import React from 'react';
import LogoutButton from "../Components/Login/LogoutButton";
import Form from "../Components/Publication/Components/Form";
import FormInputElement from "../Components/Publication/Components/FormInputElement";
import Dropdown from "../Components/Publication/Components/Dropdown";
import Button from "../Components/Publication/Components/Button";
import Table from '../Components/Publication/Components/Table';
import TableRow from '../Components/Publication/Components/TableRow';
import  { useState, useEffect } from 'react';
import PublicationService from '../services/PublicationService';
import AuthorService from '../services/AuthorService';
import PublicationTable from '../Components/Publication/Components/PublicationTable';
import { Multiselect } from 'multiselect-react-dropdown';

export default function AddPublicationPage (){

const [publication, setPublication] =useState({});

const[publicationList, setPublicationList]= useState([]);

const[authorList, setAuthorList]= useState([]);

const [enteredInputs, setEnteredInputs] = useState({ 
    title:"",
    description: "",
    edition:"",
    quantity:"",
    datePublished:"",
    numberOfPages:""
});

const [selectedLanguage, setSelectedLanguage]= useState({});

const [selectedType, setSelectedType] = useState({});

const [selectedGenre, setSelectedGenre] = useState({});

const [selectedPublisher, setSelectedPublisher] = useState({});

const [checkedAuthors, setCheckedAuthors] = useState([])

const languageOptions=[{value:"english", name:"english", id:"english", label: "English"}];

const genreOptions = [
        { value: "fashion", name: "fashion", id: "f3149263-dae9-42eb-9f56-aeee72cbe4c6", label: "Fashion" },
        { value: "science", name: "science", id: "c92fdc45-0d69-45b1-aebf-7bd5b4b2453a", label: "Science" },
        { value: "humor", name: "humor", id: "d5a745c8-1e50-48d8-9d46-5b371e5dd036", label: "Humor" },
        { value: "crime", name: "crime", id: "0d19de75-303a-4f80-8527-7ed1754ca44f", label: "Crime" },
        { value: "horror", name: "horror", id: "6c7b5872-c6b3-4154-badb-697fb4839e87", label: "Horror" },
        { value: "technology", name: "technology", id: "9d112d9a-f206-473f-8911-7fe92d883cf2", label: "Technology" },
        { value: "science_fiction", name: "science_fiction", id: "87105c89-2526-40b4-a0af-23ffb4a49151", label: "Science Fiction" },
        { value: "dictionary", name: "dictionary", id: "b9636165-27da-4597-89ea-0d102ede799c", label: "Dictionary" },
        { value: "superhero", name: "superhero", id: "c6290692-7651-4374-8701-e00e6a5661c8", label: "Superhero" },
        { value: "fantasy", name: "fantasy", id: "d8b1e577-c912-4ff4-a2df-c001ba580582", label: "Fantasy" },
        { value: "sports", name: "sports", id: "903b8ae7-27f7-4287-99b6-0c2a37f7f832", label: "Sports" },
        { value: "self_help", name: "self_help", id: "6b058428-2e07-4964-8e8f-cf87b9d6651f", label: "Self help" },
        { value: "lifestyle", name: "lifestyle", id: "3deb301a-a9eb-479e-a255-2ce42cf58e57", label: "Lifestyle" },
        { value: "nature", name: "nature", id: "d5f20053-8ebb-48ae-970b-83ffc784e919", label: "Nature" },
        { value: "adventure", name: "adventure", id: "a189f156-a681-42d2-8207-39746d82c2e6", label: "Adventure" },
        { value: "novel", name: "novel", id: "63ea9783-f8f0-45d4-a192-1d00890890b2", label: "Novel" },
        { value: "childrens_book", name: "childrens_book", id: "cbb09164-418a-495a-a72f-915bf173b842", label: "Children's Book" },
        { value: "drama", name: "drama", id: "3a36158f-5915-4477-a803-c6d80d80e3c7", label: "Drama" },
        { value: "mystery", name: "mystery", id: "9269af45-a32b-41b2-845b-439d83b4e0b2", label: "Mystery" }
      ];
    
const typeOptions = [
    { value: "book", name: "book", id: "4e1135e5-a0ef-4cb0-86a0-a98d55302b15", label: "Book" },
    { value: "dictionary", name: "dictionary", id: "4ba15d42-153c-496c-8e59-073d7288f2bf", label: "Dictionary" },
    { value: "comic_book", name: "comic_book", id: "3cd10ab1-dd8e-4e08-b44c-626555417a9b", label: "Comic Book" },
    { value: "magazine", name: "magazine", id: "4915b255-ecac-410b-8046-3d916465a75b", label: "Magazine" }
];


const publisherOptions =[{value:"Secker & Warburg", name:"Secker & Warburg", id:"a2b3b38a-79ce-409c-9686-de4396b67dec", label: "Secker & Warburg"},
{value:"Thomas Creede", name:"Thomas Creede", id:"64c3b118-b12b-4582-ad5f-df97736ce8c0", label: "Thomas Creede"}
];

useEffect( () => {
   const  params = {pageSize: 10000};
    AuthorService.getAllAuthors({params})
    .then((response) => {
        console.log(response.data);
        const authors = response.data.map((item) => {
          const author = {
            ...item
          };
          return author;
        });
        const updatedAuthorList = authors.map(author => {
            return {
              ...author,
              name: `${author.firstName} ${author.lastName}`,
            };
          });
          
        setAuthorList(updatedAuthorList);
    })
},[]);

function handleCheckboxSelect(selectedAuthor) {
    debugger;
    setCheckedAuthors((prevCheckedAuthors) => [...prevCheckedAuthors, {...selectedAuthor, id: authorList.filter(author=> author.firstName === selectedAuthor.firstName
      && author.lastName === selectedAuthor.lastName).id}]);
}

function handleCheckboxRemove(removedAuthor){
    debugger;
    setCheckedAuthors((prevCheckedAuthors) =>
    prevCheckedAuthors.filter(
        option => option.id === removedAuthor)
    );
}

function handleInputChange(e){
    e.preventDefault();
    const { name, value } = e.target;
      setEnteredInputs((prevEnteredInputs) => ({
        ...prevEnteredInputs,
        [name]: value,
      }));

}

function handleDropdownChange(e){
    e.preventDefault();
    const { value } = e.target;
    if(languageOptions.some(item => item.value === value))
    {
      const select = languageOptions.find(option => option.value === e.target.value);
      setSelectedLanguage(select);
    }
    else if(typeOptions.some(item => item.value === value))
    {
        const select = typeOptions.find(option => option.value === e.target.value);
        setSelectedType(select);
    }
    else if(genreOptions.some(item => item.value === value))
    {
        const select = genreOptions.find(option => option.value === e.target.value);
        setSelectedGenre(select);
    }

}

function handleSubmit(e){
    e.preventDefault();
        const { name, value } = e.target;
        
        const params = {...enteredInputs, 
            language : selectedLanguage.value,
            typeId : selectedType.id,
            genreId : selectedGenre.id,
            listOfAuthorIds: checkedAuthors.map(authors=>authors.id).join(',')
        };

        PublicationService.addPublication( { params })
        .then((response) => {
            console.log(response.data);
        });


}

return(
    <div>
        <LogoutButton></LogoutButton>

        <Form id="addPublicationForm" formHeader= "Add a publication">
        
        <FormInputElement name = "title"  type="text" label = "Title" handleChange = {handleInputChange}></FormInputElement>
        <FormInputElement name = "description"  type="text" label = "Description" handleChange = {handleInputChange}></FormInputElement>
        <FormInputElement name = "edition"  type="number" label = "Edition" handleChange = {handleInputChange}></FormInputElement>
        <FormInputElement name = "quantity"  type="number" label = "Quantity" handleChange = {handleInputChange}></FormInputElement>
        <FormInputElement name = "datePublished"  type="date" label = "Date Published" handleChange = {handleInputChange}></FormInputElement>
        <FormInputElement name = "numberOfPages"  type="number" label = "Number Of Pages" handleChange = {handleInputChange}></FormInputElement>

        <Dropdown name="language" options = {languageOptions} label="Language" selectedOption={selectedLanguage.value} handleChange={handleDropdownChange}></Dropdown>
        <Dropdown name="type" options = {typeOptions} label="Type" selectedOption={selectedType.value} handleChange={handleDropdownChange}></Dropdown>
        <Dropdown name="genre" options = {genreOptions} label="Genre" selectedOption={selectedGenre.value} handleChange={handleDropdownChange}></Dropdown>
        
        
        <Multiselect
          options={authorList} // Options to display in the dropdown
          selectedValues={checkedAuthors}// Preselected value to persist in dropdown
          onSelect={handleCheckboxSelect} // Function will trigger on select event
          onRemove={handleCheckboxRemove} // Function will trigger on remove event
          displayValue="name"// Property name to display in the dropdown options
        />
        <Button label="Add" handleClick={handleSubmit}></Button>
        </Form>
    </div>
)

}

