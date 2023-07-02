import React from 'react';
import axios from 'axios';
import PublicationService from '../services/PublicationService';
import AuthorService from '../services/AuthorService';
import  { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import Table from '../Components/Publication/Components/Table';
import TableRow from '../Components/Publication/Components/TableRow';




function GetAuthorByIdPage(){

const { id } = useParams();

const [author, setAuthor] = useState({});



useEffect(() => {
AuthorService.getAuthorById(id).then(response => {
    console.log(response.data);
    setAuthor(response.data);
  })
  .catch(error => {
        console.log(error);
  });
},[id])



return (
    <div>
      <h2> Author</h2>
      <br></br>
      <Table tableName= "Author Table" columnNames={['First Name', 'Last Name', 'Nationality', 'Date Of Birth', 'Date Of Death']}>
          <TableRow key={author.id} rowData={[author.firstName, author.lastName, author.nationality, author.dateOfBirth, author.dateOfDeath]} />
      </Table>
    </div>
);

}

export default GetAuthorByIdPage;



