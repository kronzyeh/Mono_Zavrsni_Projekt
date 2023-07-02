import axios from "axios";

const getAllAuthors= (data) => {
    return axios.get("https://localhost:44389/api/library/authors", data);
  };

const getAuthorById = (id) =>{
    return axios.get(`https://localhost:44389/api/library/author/${id}`)
}

const addAuthor = (data) => {
    return axios.post("https://localhost:44389/api/library/add/author", data);
  };

  const AuthorService = {
    getAllAuthors,
    addAuthor,
    getAuthorById
  };
export default AuthorService;