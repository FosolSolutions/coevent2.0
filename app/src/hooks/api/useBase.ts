import { useSummon } from 'hooks';

/**
 * Common hook to make requests to the APi.
 * @returns CustomAxios object setup for the API.
 */
export const useBase = () => {
  const { summon } = useSummon();

  return summon;
};

export default useBase;
