import { MenuButton as MB } from '@szhsin/react-menu';
import styled from 'styled-components';

export const MenuButton = styled(MB)`
  color: inherit;
  background-color: ${(props) => props.theme.css.primaryLightColorRgb};
  cursor: pointer;
  font-size: 1rem;
  font-weight: 500;
  padding: 0.375rem 0.75rem;
  line-height: 1.5;
  border-radius: 0.25rem;
  border: 0px solid #ddd;
  transition-property: background-color, border-color, color, box-shadow;
  transition-duration: 0.15s;
  transition-timing-function: ease-in-out;

  &:hover {
    background-color: ${(props) => props.theme.css.primaryColorRgb};
  }

  &:active {
    background-color: ${(props) => props.theme.css.primaryColorRgb};
  }
`;
