import { Counter } from "./components/Counter";
import { FetchData } from "./components/FetchData";
import { UseClients } from "./components/UseClients";
import { Home } from "./components/Home";

const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  {
    path: '/counter',
    element: <Counter />
  },
  {
    path: '/fetch-data',
    element: <FetchData />
  },
  {
    path: '/use-clients',
    element: <UseClients />
  }
];

export default AppRoutes;
