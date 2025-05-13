import { Navigate } from "react-router-dom";

export default function PrivateRoute({ children }) {
  const auth = localStorage.getItem("token") != null ? true : false; 

  return <>{auth ? children : <Navigate to="/" />}</>;
}
