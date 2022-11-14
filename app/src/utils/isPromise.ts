/**
 * Determine if the specified 'obj' is a Promise.
 * @param obj The object to test.
 * @returns true if it is a Promise.
 */
export const isPromise = (obj: any) => {
  return typeof obj === 'object' && typeof obj.then === 'function';
};
