import styled from 'styled-components';

export const EventActivityStyled = styled.div`
  min-width: 100px;
  padding: 0.25em;
  border-radius: 0.25em;
  overflow: hidden;
  text-align: center;
  display: flex;
  flex-direction: column;
  gap: 0.25em;
  flex: 1 1 auto;
  background-color: ${(props) => props.theme.css.light};

  h3 {
    margin: 0;
  }

  :hover {
    background-color: ${(props) => props.theme.css.white};
  }

  .opening:last-child {
    height: 100%;
  }
`;
