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
import PublicationTable from '../Components/Publication/Components/PublicationTable';
import AuthorService from '../services/AuthorService';
import Header from '../Components/Header/Header';


export default function HomePage (){

    const [publication, setPublication] =useState({});

    const[publicationList, setPublicationList]= useState([]);

    const [isLoading, setIsLoading] = useState(true);

    
    const [enteredInputs, setEnteredInputs] = useState({ 
        searchQuery: "",
        minDatePublished:"",
        maxDatePublished: "",
        minNumberOfPages: "",
        maxNumberOfPages: ""
    });

    const [selectedLanguage, setSelectedLanguage]= useState({});

    const [selectedType, setSelectedType] = useState({});

    const [selectedGenre, setSelectedGenre] = useState({});

    const [selectedOrderBy, setSelectedOrderBy] = useState({});

    const [selectedSortOrder, setSelectedSortOrder] = useState({});

    const languageOptions=[{value:"english", name:"english", id:"english", label: "English"}];

    const genreOptions = [
      { value: "childrens_book", name: "childrens_book", id: "8aa08113-f25e-48ca-91b2-16c5176061db", label: "Children's Book" },
      { value: "science_fiction", name: "science_fiction", id: "c765179b-178f-4217-b42b-8c421c3e5d8f", label: "Science Fiction" },
      { value: "novel", name: "novel", id: "9f274d60-b010-4252-a2e6-e2d2ac4e0a55", label: "Novel" },
      { value: "dictionary", name: "dictionary", id: "10905a86-5c71-4319-81e1-869be8fa8130", label: "Dictionary" },
      { value: "drama", name: "drama", id: "d414b41f-a809-4bcb-95ed-fc90d49208ea", label: "Drama" },
      { value: "crime", name: "crime", id: "c38ab1b4-4cee-4162-bea0-e55ed88ab836", label: "Crime" },
      { value: "horror", name: "horror", id: "e9c74aa7-5bf6-460f-ba7a-da0ee4472c87", label: "Horror" },
      { value: "self_help", name: "self_help", id: "a0434e55-9e7d-4ef9-be61-7286882109eb", label: "Self help" },
      { value: "fashion", name: "fashion", id: "a382f495-23be-464f-a379-286f89df5336", label: "Fashion" },
      { value: "sports", name: "sports", id: "da84807d-ecd7-4ab2-8859-1af58709621b", label: "Sports" },
      { value: "lifestyle", name: "lifestyle", id: "3f0b5498-60f8-4dac-bd18-631616b78d8d", label: "Lifestyle" },
      { value: "technology", name: "technology", id: "10789dc1-1959-49b8-bf97-71c12bddb70c", label: "Technology" },
      { value: "science", name: "science", id: "e3097bf2-8be4-4ace-9491-3ebe71887fd3", label: "Science" },
      { value: "nature", name: "nature", id: "45edc4db-c789-40e8-ba55-12bd9d67c6f1", label: "Nature" },
      { value: "humor", name: "humor", id: "39f89ba7-4abe-4275-bbd3-4ba2308a918c", label: "Humor" },
      { value: "adventure", name: "adventure", id: "1cfdf8e2-0f08-4108-9205-05a721eb90e3", label: "Adventure" },
      { value: "superhero", name: "superhero", id: "1cccfe9d-202e-472f-aeab-85682521b357", label: "Superhero" },
      { value: "mystery", name: "mystery", id: "33faa970-1391-46ff-a958-acd055db05ca", label: "Mystery" },
      { value: "fantasy", name: "fantasy", id: "109334b2-f0c9-4327-955e-642c3e976bfc", label: "Fantasy" },
    ];
    
    
    const typeOptions = [
      { value: "book", name: "book", id: "ea8fe87a-c6d8-41a9-95e0-0526d83e23c0", label: "Book" },
      { value: "magazine", name: "magazine", id: "ff289d2b-d283-4cfd-8757-231cbdbe9197", label: "Magazine" },
      { value: "comic_book", name: "comic_book", id: "49db9705-3a70-46f6-aac9-77fbcb1905ab", label: "Comic Book" },
      { value: "dictionary", name: "dictionary", id: "f73146bb-6658-4868-8a31-cc7d0548414e", label: "Dictionary" }
    ];
    

    const sortOrderOptions=[{value:"asc", name:"asc", id:"asc", label: "Ascending"},
    {value:"desc", name:"desc", id:"desc", label: "Descending"}]

    const orderByOptions=[{value:"title", name:"title", id:"title", label: "Title"},
    {value:"datePublished", name:"datePublished", id:"datePublished", label: "Date Published"},
    {value:"numberOfPages", name:"numberOfPages", id:"numberOfPages", label: "Number Of Pages"},
    {value:"quantity", name:"quantity", id:"quantity", label: "Quantity"}
    ]
     
    useEffect(() => {
      setIsLoading(true);
      const params = { sortOrder: "desc", sortBy: "DatePublished", pageNumber: 1, pageSize: 10 };
      PublicationService.getAllPublications(params)
        .then((response) => {
          console.log(response.data.publications);
          
          const allPublications = response.data.publications.map((item) => {
            const publication = {
              ...item,
              type: typeOptions.find((option) => option.id === item.typeId)?.label || "Unknown Type",
              genre: genreOptions.find((option) => option.id === item.genreId)?.label || "Unknown Genre",
              datePublished: item.datePublished.slice(0, -9)
            };
          


            return publication;
          });
    
          setPublicationList(allPublications);
          setIsLoading(false);
        })
        .catch((error) => {
          console.log(error);
          setIsLoading(false);
        });
    }, []);
    
    



    function handleDropdownChange(e) 
    {
        e.preventDefault();
        const { value } = e.target;
        
        if(languageOptions.some(item => item.value === value))
        {
          const select = languageOptions.find(option => option.value === e.target.value);
          setSelectedLanguage(select);
        }
        else if(orderByOptions.some(item => item.value === value))
        {
            const select = orderByOptions.find(option => option.value === e.target.value);
            setSelectedOrderBy(select);
        }
        else if(sortOrderOptions.some(item => item.value === value))
        {
            const select = sortOrderOptions.find(option => option.value === e.target.value);
            setSelectedSortOrder(select);
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

    function handleInputChange(e)
    {
        e.preventDefault();
        const { name, value } = e.target;
          setEnteredInputs((prevEnteredInputs) => ({
            ...prevEnteredInputs,
            [name]: value,
          }));
    }

    function handleSubmit(e){
        e.preventDefault();
        const { name, value } = e.target;
        
        const params = {...enteredInputs, 
            language : selectedLanguage.value,
            typeId : selectedType.id,
            genreId : selectedGenre.id,
            orderBy: selectedOrderBy.value,
            sortOrder: selectedSortOrder.value
        };
        PublicationService.getAllPublications(params)
        .then((response) => {
          console.log(response.data.publications);
    
          const allPublications = response.data.publications.map((item) => {
            const publication = {
              ...item,
              type: typeOptions.find((option) => option.id === item.typeId)?.label || "Unknown Type",
              genre: genreOptions.find((option) => option.id === item.genreId)?.label || "Unknown Genre",
            };
            return publication;
          });
    
          setPublicationList(allPublications);
          setIsLoading(false);
        })
    }


    return(
        
        <div>
          <header>
            <Header/>
          </header>

            <Form id="searchPublicationsForm" formHeader= "Search publications">

            <FormInputElement name = "searchQuery"  type="text" label = "" handleChange = {handleInputChange}></FormInputElement>
            
            <div>
                <h5> Date Published</h5>
            <FormInputElement name = "minDatePublished"  type="date" label = "From" handleChange = {handleInputChange}></FormInputElement>
            <FormInputElement name = "maxDatePublished"  type="date" label = "To" handleChange = {handleInputChange}></FormInputElement>
            </div>

            <div>
                <h5>Number Of Pages</h5>
            <FormInputElement name = "minNumberOfPages"  type="number" label = "Minimum" handleChange = {handleInputChange}></FormInputElement>
            <FormInputElement name = "maxNumberOfPages"  type="number" label = "Maximum" handleChange = {handleInputChange}></FormInputElement>
            </div>

            <Dropdown name="language" options = {languageOptions} label="Language" selectedOption={selectedLanguage.value} handleChange={handleDropdownChange}></Dropdown>
            <Dropdown name="type" options = {typeOptions} label="Type" selectedOption={selectedType.value} handleChange={handleDropdownChange}></Dropdown>
            <Dropdown name="genre" options = {genreOptions} label="Genre" selectedOption={selectedGenre.value} handleChange={handleDropdownChange}></Dropdown>

            <Dropdown name="sortOrder" options = {sortOrderOptions} label="Sort Order" selectedOption={selectedSortOrder.value} handleChange={handleDropdownChange}></Dropdown>
            <Dropdown name="orderBy" options = {orderByOptions} label="Order By" selectedOption={selectedOrderBy.value} handleChange={handleDropdownChange}></Dropdown>


            <Button label="Search" handleClick={handleSubmit}></Button>

            </Form>

            {isLoading ? (<p>Loading...</p>):(
            <Table
            tableName="Publications based on your filters"
            columnNames={['Title', 'Type', 'Genre', 'Number Of Pages','Date Published', 'Quantity']}>
            {publicationList.map((publication) => (
              <TableRow
                key={publication.id}
                rowData={[
                  publication.title,
                  publication.type,
                  publication.genre,
                  publication.numberOfPages,
                  publication.datePublished,
                  publication.quantity
                ]}
              />
            ))}
          </Table>)}

        </div>

        

    )

}