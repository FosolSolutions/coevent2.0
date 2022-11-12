import styled from 'styled-components';

export const Menu = styled.div`
  display: flex;
  flex-direction: row;
  width: 100%;

  a {
    text-decoration: none;
    color: inherit;
  }

  & > button {
    margin-left: 0.25em;
  }
`;
