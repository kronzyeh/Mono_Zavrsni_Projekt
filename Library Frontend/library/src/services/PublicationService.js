import axios from "axios";

const getAllPublications = (data) => {
    return axios.get("https://localhost:44389/api/library/home", data);
  };

  const getPublicationById = (id) => {
    return axios.get(`https://localhost:44389/api/library/publication/${id}`);
  };


const addPublication = (data) => {
    return axios.post("https://localhost:44389/api/library/add/publication", data);
  };

  const PublicationService = {
    getAllPublications,
    addPublication,
    getPublicationById
  };
export default PublicationService;