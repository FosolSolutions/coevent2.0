import styled from 'styled-components';

export const ScheduleEvent = styled.div`
  display: flex;
  flex-direction: row;
  gap: 1em;

  background-color: ${(props) => props.theme.css.filterBackgroundColor};
  border-radius: 0.25em;
  padding: 0.5em;

  h2 {
    margin: 0;
  }

  .event > .activities {
    display: flex;
    flex-direction: row;
    gap: 1em;
    flex-wrap: wrap;

    > * {
      flex: 1 1 auto;
    }
  }
`;
