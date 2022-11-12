import styled from 'styled-components';

export const Calendar = styled.div`
  display: flex;
  flex-direction: column;
  flex-wrap: wrap;
  align-items: stretch;
  align-content: stretch;
  gap: 0.5em;

  .c-header {
    .c-day {
      align-items: center;
      min-height: auto;
    }
  }

  .c-week {
    display: flex;
    flex-direction: row;
    flex-wrap: wrap;
    align-items: center;
    align-content: stretch;
    justify-content: stretch;
    gap: 0.5em;
    justify-content: center;
    flex-grow: 1;
  }

  .c-day {
    display: flex;
    flex-direction: column;
    align-items: stretch;
    align-content: stretch;
    border: 1px solid #fff;
    flex: 1 1 0px;
    min-height: 10em;
    border-radius: 0.25em;
    background-color: ${(props) => props.theme.css.tableColor};

    &:hover {
      filter: brightness(90%);
    }
  }
`;

export default Calendar;
