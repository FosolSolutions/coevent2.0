import { useSummon } from 'hooks';

/**
 * Common hook to make requests to the PIMS APi.
 * @returns CustomAxios object setup for the PIMS API.
 */
export const useBase = () => {
  const { summon } = useSummon();

  return summon;
};

export default useBase;
