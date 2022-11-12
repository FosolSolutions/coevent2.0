import styled from 'styled-components';

export const Schedule = styled.div`
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

  .month {
    display: flex;
    flex-direction: column;
    gap: 0.5rem;
    background-color: #fff;
    border-radius: 0.5rem;
    padding: 0.5rem;
  }

  .date {
    display: flex;
    flex-direction: row;
    gap: 1rem;
    background-color: grey;
    border-radius: 0.5rem;
    padding: 1rem;
  }

  .events {
    display: flex;
    flex-direction: column;
    flex-grow: 1;
    gap: 1rem;
  }

  .event {
    display: flex;
    flex-direction: row;
    align-items: stretch;
    align-content: stretch;
    gap: 1rem;
    background-color: #fff;
    border-radius: 0.5rem;
    padding: 1rem;
  }

  .opening {
    display: flex;
    flex-direction: column;
    flex-grow: 1;
    gap: 1rem;

    ul {
      display: flex;
      flex-direction: column;
      gap: 1rem;

      li {
        align-items: stretch;
        align-content: stretch;
        span {
          margin: 0.5rem 0.75rem 0.5rem 0.5rem;
        }
      }
    }
  }
`;

export default Schedule;
