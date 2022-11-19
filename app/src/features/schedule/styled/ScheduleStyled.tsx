import styled from 'styled-components';

export const Schedule = styled.div`
  display: flex;
  flex-direction: column;
  margin: 0 1em 0 1em;
  width: 100%;

  nav {
    display: flex;
    flex-direction: row;
    margin: 0.75em;

    ul {
      display: flex;
      flex-direction: row;
      list-style-type: none;
    }
  }
`;

export default Schedule;
