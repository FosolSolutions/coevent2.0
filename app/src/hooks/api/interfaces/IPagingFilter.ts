type WithAny<T extends any> = T;

export interface IPagingFilter extends WithAny<any> {
  page?: number;
  quantity?: number;
}
