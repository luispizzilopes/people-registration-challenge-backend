import { Card } from 'primereact/card';
import Header from '../Header';

import "./style.css";

export default function DefaultPage({ title, icon, children }) {
  return (
    <div className="flex flex-column flex-1" style={{ minHeight: '97.5vh' }}>
      <Header />

      <div className="flex flex-column flex-1 px-1 py-1">
        <Card className="flex flex-column flex-1">
          <div className="flex align-items-center gap-2">
            {icon}
            <h1 className="text-xl m-0">{title}</h1>
          </div>

          <div className={`my-3 border-top-1 surface-border`} />

          <div className="flex-1 h-full">
            {children}
          </div>
        </Card>
      </div>

      <style global jsx>{`
        .p-datatable .p-datatable-thead > tr > th {
          background-color: var(--primary-color); /* Cor primária do tema */
          color: var(--primary-color-text); /* Cor do texto primário */
          text-align: center; /* Centraliza o texto do cabeçalho */
          font-weight: bold; /* Negrito no texto */
        }

        .p-sortable-column-icon {
          color: var(--primary-color-text); /* Cor do texto primário */
        }
      `}</style>
    </div>
  );
}