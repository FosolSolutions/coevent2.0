import styled from 'styled-components';

export const EventActivityStyled = styled.div`
  min-width: 100px;
  padding: 0.25em;
  border-radius: 0.25em;

  :hover {
    span {
      color: ${(props) => props.theme.css.activeColor};
    }

    background-color: ${(props) => props.theme.css.light};
  }
`;
