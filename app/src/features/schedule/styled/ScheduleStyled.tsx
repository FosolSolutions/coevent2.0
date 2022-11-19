import styled from 'styled-components';

export const Schedule = styled.div`
  margin: 0 1em 0 1em;

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

  .schedule {
    display: flex;
    flex-direction: column;
    gap: 0.5rem;
  }
`;

export default Schedule;
