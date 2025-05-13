import { createRoot } from 'react-dom/client'
import App from './App.jsx'
import { addLocale } from "primereact/api";
import { ToastContainer } from 'react-toastify';
import "primereact/resources/primereact.min.css";
import "primeflex/primeflex.css";
import "primeicons/primeicons.css";
import "primereact/resources/themes/lara-light-blue/theme.css";
import "./GlobalStyle.css";

addLocale("ptbr", {
  firstDayOfWeek: 0,
  dayNames: [
    "Domingo",
    "Segunda",
    "Terça",
    "Quarta",
    "Quinta",
    "Sexta",
    "Sábado",
  ],
  dayNamesShort: ["Dom", "Seg", "Ter", "Qua", "Qui", "Sex", "Sáb"],
  dayNamesMin: ["D", "S", "T", "Q", "Q", "S", "S"],
  monthNames: [
    "Janeiro",
    "Fevereiro",
    "Março",
    "Abril",
    "Maio",
    "Junho",
    "Julho",
    "Agosto",
    "Setembro",
    "Outubro",
    "Novembro",
    "Dezembro",
  ],
  monthNamesShort: [
    "Jan",
    "Fev",
    "Mar",
    "Abr",
    "Mai",
    "Jun",
    "Jul",
    "Ago",
    "Set",
    "Out",
    "Nov",
    "Dez",
  ],
  today: "Hoje",
  clear: "Limpar",
  weekHeader: "Semana",
  searchPlaceholder: "Pesquisar",
  emptyMessage: "Nenhum resultado encontrado",
  noResultsFound: "Nenhum resultado encontrado",
  paginator: {
    first: "Primeiro",
    last: "Último",
    rows: "Linhas por página",
    of: "de",
  },
});

createRoot(document.getElementById('root')).render(
  <>
    <App />
    <ToastContainer
      position="top-right"
      autoClose={5000}
      hideProgressBar={false}
      newestOnTop={false}
      closeOnClick={false}
      rtl={false}
      pauseOnFocusLoss
      draggable
      pauseOnHover
      theme="light"
    />
  </>
)
