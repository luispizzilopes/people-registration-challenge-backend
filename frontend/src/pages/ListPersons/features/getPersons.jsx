import { toast } from "react-toastify";
import api from "../../../services/api";

export default async function getPersons(setPersons){
    try{
        const response = await api.get("v1/Person"); 

        setPersons(response.data);
    }
    catch(error){
        if(error?.response?.status == 401 || error?.response?.status == 403)
            return; 
        
        toast.error("Ocorreu um erro ao tentar carregar as pessoas cadastradas!");
    }
}