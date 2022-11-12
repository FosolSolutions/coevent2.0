import styled from 'styled-components';

export const Logo = styled.div`
  height: 50px;

  svg path {
    fill: ${(props) => props.theme.css.primaryColor} !important;
  }
`;
