import history from "../lib/history";

export default function signOut(){
    localStorage.clear(); 
    history.push("/");
}