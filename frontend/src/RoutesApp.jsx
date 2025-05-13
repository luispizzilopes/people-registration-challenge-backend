import { Route, Routes } from "react-router-dom";
import { unstable_HistoryRouter as HistoryRouter } from "react-router-dom";
import history from "./lib/history";
import Login from "./pages/login";
import Register from "./pages/Register";
import ListPersons from "./pages/ListPersons";
import PrivateRoute from "./routes/PrivateRoute";
import Unauthorized from "./pages/Unauthorized";
import NotFound from "./pages/NotFound";
import Unavailable from "./pages/Unavailable";

export default function RoutesApp() {
    return (
        <HistoryRouter history={history}>
            <Routes>
                <Route path="*" element={<NotFound/>} />
                <Route path="/unauthorized" element={<Unauthorized />} />
                <Route path="/unavailable" element={<Unavailable />} />

                <Route path="/" element={<Login />} />
                <Route path="/register" element={<Register />} />

                <Route path="/listPersons" element={<PrivateRoute><ListPersons /></PrivateRoute>} />
            </Routes>
        </HistoryRouter>
    )
}