import { FaSignOutAlt } from 'react-icons/fa';
import styled from 'styled-components';

export const LogoutButton = styled(FaSignOutAlt)`
  max-width: 20px;
  cursor: pointer;

  &:hover {
    color: ${(props) => props.theme.css.danger};
  }
`;

export default LogoutButton;
