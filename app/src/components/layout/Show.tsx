export interface IShowProps {
  on?: boolean;
  children: React.ReactNode;
}

export const Show: React.FC<IShowProps> = ({ on = false, children }) => {
  return on ? <>{children}</> : null;
};
