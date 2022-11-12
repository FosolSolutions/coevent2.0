import { MenuItem, MenuItemProps } from '@szhsin/react-menu';
import { useNavigate } from 'react-router-dom';

/**
 * MenuLink properties.
 */
export interface IMenuLinkProps extends MenuItemProps {
  /**
   * URL route path.
   */
  to: string;
}

/**
 * MenuLink provides a menu link component that uses React Router.
 * @param param0 Properties of component.
 * @returns MenuLink component.
 */
export const MenuLink: React.FC<IMenuLinkProps> = ({ to, ...rest }) => {
  const navigate = useNavigate();

  return (
    <MenuItem
      href={to}
      onClick={(e) => {
        e.syntheticEvent.preventDefault();
        navigate(to);
      }}
      {...rest}
    ></MenuItem>
  );
};
