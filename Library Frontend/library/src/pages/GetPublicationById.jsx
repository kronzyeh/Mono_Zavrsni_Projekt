import React from 'react';
import axios from 'axios';
import PublicationService from '../services/PublicationService';
import  { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import Table from '../Components/Publication/Components/Table';
import TableRow from '../Components/Publication/Components/TableRow';
import AuthorService from '../services/AuthorService';



function GetPublicationByIdPage(){

const { id } = useParams();

const [publication, setPublication] = useState({});

const [authorOptions, setAuthorOptions]= useState({});

const [listOfAuthors, setListOfAuthors]= useState([]);


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



useEffect(() => {
PublicationService.getPublicationById(id).then(response => {
    console.log(response.data);
    setPublication(response.data);
  })
  .catch(error => {
        console.log(error);
  });
},[id])

useEffect(()=>{
    AuthorService.getAllAuthors()
          .then((response) => {
            console.log(response.data);
            debugger;
            const allAuthors = response.data.map((item) => {
              const author = {
                ...item,
                fullName: `${item.firstName} ${item.lastName}`
              };
              return author;
            });
            debugger;
            setAuthorOptions(allAuthors);

           
            publication.listOfAuthorIds.map((authorId) => {
                const author = authorOptions.find((author) => author.id === authorId);
                if (author) {
                  return author.fullName;
                }
                return null; 
              });
        })
    }, [])


return (
    <div>
      <h2> Publication</h2>
      <br></br>
      <Table tableName= "Publication Table" columnNames={['Title', 'Authors', 'Type', 'Genre', 'Number Of Pages','Date Published', 'Quantity']}>
          <TableRow key={publication.id} 
          rowData={[
            publication.title,
            publication.listOfAuthorIds,
            publication.type,
            publication.genre,
            publication.numberOfPages,
            publication.datePublished,
            publication.quantity
          ]} />
      </Table>
    </div>
);

}

export default GetPublicationByIdPage;



