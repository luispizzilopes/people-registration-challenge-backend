import { toast } from "react-toastify";
import api from "../../../services/api";
import getPersons from "./GetPersons";

export default async function addPerson(person, setOpen, setPersons) {
    try {
        const response = await api.post("v1/Person", person);

        toast.success(response.data.successMessage);
    
        await getPersons(setPersons);
         
        setTimeout(() => {
            setOpen(false);
        }, 1000);
    }
    catch (error) {
        toast.error(error.response.data.errorMessage ?? "Ocorreu um erro ao tentar adicionar uma nova pessoa!");
    }
}