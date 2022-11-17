export interface IShowProps {
  on: boolean;
  children: React.ReactNode;
}

export const Show: React.FC<IShowProps> = ({ on, children }) => {
  return on ? <>{children}</> : null;
};
