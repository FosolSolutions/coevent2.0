import styled from 'styled-components';

export const Schedule = styled.div`
  margin: 0 1em 0 1em;

  nav {
    display: flex;
    flex-direction: row;
    margin: 1rem;

    ul {
      display: flex;
      gap: 1rem;
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
