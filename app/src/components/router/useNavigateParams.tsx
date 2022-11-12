import axios from 'axios';
import { useNavigate } from 'react-router-dom';

type TUseNavigateParams = {
  uri: string;
  params?: Record<string, unknown>;
};

export default function useNavigateParams() {
  const navigate = useNavigate();

  return ({ uri, params = {} }: TUseNavigateParams) => {
    const path = axios.getUri({ url: uri, params });

    navigate(path);
  };
}
