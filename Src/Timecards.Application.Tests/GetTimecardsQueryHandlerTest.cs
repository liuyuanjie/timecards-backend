using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using MockQueryable.Moq;
using Moq;
using Timecards.Application.Extensions;
using Timecards.Application.Interfaces;
using Timecards.Application.Query.Timecards;
using Timecards.Domain;
using Xunit;

namespace Timecards.Application.Tests
{
    public class GetTimecardsQueryHandlerTest
    {
        private readonly Mock<IRepository<Domain.Timecards>> _timecardsRepositoryMock;
        private readonly Mock<IRepository<Domain.Project>> _projectRepositoryMock;
        private readonly GetTimecardsQueryHandler _getTimecardsQueryHandler;
        private readonly Guid _accountId;
        private readonly Guid _projectId;
        private readonly DateTime _mondayInCurrentWeek;
        private const int DaysInWeek = 7;

        public GetTimecardsQueryHandlerTest()
        {
            _projectRepositoryMock = new Mock<IRepository<Project>>();
            _timecardsRepositoryMock = new Mock<IRepository<Domain.Timecards>>();
            _accountId = new Guid("00000000-0000-0000-0000-00000000000a");
            _projectId = new Guid("00000000-0000-0000-0000-00000000000d");
            _mondayInCurrentWeek = DateTime.Today.ToUniversalTime().GetFirstDayOfWeek();

            _getTimecardsQueryHandler =
                new GetTimecardsQueryHandler(_timecardsRepositoryMock.Object, _projectRepositoryMock.Object);
            _projectRepositoryMock.Setup(x => x.Query())
                .Returns(new List<Project>
                {
                    new Project
                    {
                        Id = _projectId,
                        Name = "Test Project"
                    }
                }.AsQueryable());
        }

        [Fact]
        public async void Should_Return_Correct_Timecards_Given_Has_One_Timecards_When_AccountId_Then_Monday()
        {
            var query = new GetTimecardsQuery
            {
                UserId = _accountId,
                TimecardsDate = _mondayInCurrentWeek
            };

            var timecardses = new List<Domain.Timecards>
            {
                MockTimecards.CreateTimecardsMockData(_accountId, _mondayInCurrentWeek, _projectId)
            };
            _timecardsRepositoryMock.Setup(x => x.Query())
                .Returns(timecardses.AsQueryable().BuildMock().Object);

            var result = await _getTimecardsQueryHandler.Handle(query, CancellationToken.None);

            Assert.Equal(1, result.Count());
        }

        [Fact]
        public async void Should_Return_Correct_Timecards_Given_Has_One_Timecards_When_AccountId_Then_Friday()
        {
            var query = new GetTimecardsQuery
            {
                UserId = _accountId,
                TimecardsDate = _mondayInCurrentWeek.AddDays(4)
            };

            var timecardses = new List<Domain.Timecards>
            {
                MockTimecards.CreateTimecardsMockData(_accountId, _mondayInCurrentWeek, _projectId)
            };
            _timecardsRepositoryMock.Setup(x => x.Query())
                .Returns(timecardses.AsQueryable().BuildMock().Object);

            var result = await _getTimecardsQueryHandler.Handle(query, CancellationToken.None);

            Assert.Equal(1, result.Count());
        }

        [Fact]
        public async void Should_Return_Correct_Timecards_Given_Has_One_Timecards_When_AccountId_Then_Sunday()
        {
            var query = new GetTimecardsQuery
            {
                UserId = _accountId,
                TimecardsDate = _mondayInCurrentWeek.AddDays(6)
            };

            var timecardses = new List<Domain.Timecards>
            {
                MockTimecards.CreateTimecardsMockData(_accountId, _mondayInCurrentWeek, _projectId)
            };
            _timecardsRepositoryMock.Setup(x => x.Query())
                .Returns(timecardses.AsQueryable().BuildMock().Object);

            var result = await _getTimecardsQueryHandler.Handle(query, CancellationToken.None);

            Assert.Equal(1, result.Count());
        }

        [Fact]
        public async void
            Should_Return_Correct_Timecards_Has_Before_And_After_Weeks_Timecards_Given_AccountId_Then_Monday()
        {
            var query = new GetTimecardsQuery
            {
                UserId = _accountId,
                TimecardsDate = _mondayInCurrentWeek
            };

            var timecardses = new List<Domain.Timecards>
            {
                MockTimecards.CreateTimecardsMockData(_accountId, _mondayInCurrentWeek.AddDays(-7), _projectId),
                MockTimecards.CreateTimecardsMockData(_accountId, _mondayInCurrentWeek, _projectId),
                MockTimecards.CreateTimecardsMockData(_accountId, _mondayInCurrentWeek.AddDays(7), _projectId),
            };
            _timecardsRepositoryMock.Setup(x => x.Query())
                .Returns(timecardses.AsQueryable().BuildMock().Object);

            var result = await _getTimecardsQueryHandler.Handle(query, CancellationToken.None);

            Assert.Equal(1, result.Count());
            Assert.Equal(_mondayInCurrentWeek, result.First().TimecardsDate.GetFirstDayOfWeek());
        }

        [Fact]
        public async void
            Should_Return_Correct_Timecards_Has_Before_And_After_Weeks_Timecards_Given_AccountId_Then_Sunday()
        {
            var query = new GetTimecardsQuery
            {
                UserId = _accountId,
                TimecardsDate = _mondayInCurrentWeek.AddDays(6)
            };

            var timecardses = new List<Domain.Timecards>
            {
                MockTimecards.CreateTimecardsMockData(_accountId, _mondayInCurrentWeek.AddDays(-7), _projectId),
                MockTimecards.CreateTimecardsMockData(_accountId, _mondayInCurrentWeek, _projectId),
                MockTimecards.CreateTimecardsMockData(_accountId, _mondayInCurrentWeek.AddDays(7), _projectId),
            };
            _timecardsRepositoryMock.Setup(x => x.Query())
                .Returns(timecardses.AsQueryable().BuildMock().Object);

            var result = await _getTimecardsQueryHandler.Handle(query, CancellationToken.None);

            Assert.Equal(1, result.Count());
            Assert.Equal(_mondayInCurrentWeek, result.First().TimecardsDate.GetFirstDayOfWeek());
        }

        [Fact]
        public async void
            Should_Return_Correct_Timecards_When_Has_Other_Accounts_Timecards_Given_AccountId_Then_Monday()
        {
            var query = new GetTimecardsQuery
            {
                UserId = _accountId,
                TimecardsDate = _mondayInCurrentWeek
            };

            var timecardses = new List<Domain.Timecards>
            {
                MockTimecards.CreateTimecardsMockData(_accountId, _mondayInCurrentWeek, _projectId),
                MockTimecards.CreateTimecardsMockData(Guid.NewGuid(), _mondayInCurrentWeek, _projectId),
                MockTimecards.CreateTimecardsMockData(Guid.NewGuid(), _mondayInCurrentWeek, _projectId),
            };
            _timecardsRepositoryMock.Setup(x => x.Query())
                .Returns(timecardses.AsQueryable().BuildMock().Object);

            var result = await _getTimecardsQueryHandler.Handle(query, CancellationToken.None);

            Assert.Equal(1, result.Count());
        }

        [Fact]
        public async void
            Should_Return_Correct_Timecards_When_Has_Multiple_Timecards_One_Week_Given_AccountId_Then_Monday()
        {
            var query = new GetTimecardsQuery
            {
                UserId = _accountId,
                TimecardsDate = _mondayInCurrentWeek
            };

            var timecardses = new List<Domain.Timecards>
            {
                MockTimecards.CreateTimecardsMockData(_accountId, _mondayInCurrentWeek, _projectId),
                MockTimecards.CreateTimecardsMockData(_accountId, _mondayInCurrentWeek, Guid.NewGuid()),
                MockTimecards.CreateTimecardsMockData(_accountId, _mondayInCurrentWeek, Guid.NewGuid()),
            };
            _timecardsRepositoryMock.Setup(x => x.Query())
                .Returns(timecardses.AsQueryable().BuildMock().Object);

            var result = await _getTimecardsQueryHandler.Handle(query, CancellationToken.None);

            Assert.Equal(3, result.Count());
        }

        [Fact]
        public async void
            Should_Return_Correct_Timecards_When_Has_Multiple_Timecards_One_Week_And_Other_Accounts_Given_AccountId_Then_Monday()
        {
            var query = new GetTimecardsQuery
            {
                UserId = _accountId,
                TimecardsDate = _mondayInCurrentWeek
            };

            var timecardses = new List<Domain.Timecards>
            {
                MockTimecards.CreateTimecardsMockData(_accountId, _mondayInCurrentWeek, _projectId),
                MockTimecards.CreateTimecardsMockData(_accountId, _mondayInCurrentWeek, Guid.NewGuid()),
                MockTimecards.CreateTimecardsMockData(_accountId, _mondayInCurrentWeek, Guid.NewGuid()),
                MockTimecards.CreateTimecardsMockData(Guid.NewGuid(), _mondayInCurrentWeek, _projectId),
                MockTimecards.CreateTimecardsMockData(Guid.NewGuid(), _mondayInCurrentWeek, Guid.NewGuid()),
            };
            _timecardsRepositoryMock.Setup(x => x.Query())
                .Returns(timecardses.AsQueryable().BuildMock().Object);

            var result = await _getTimecardsQueryHandler.Handle(query, CancellationToken.None);

            Assert.Equal(3, result.Count());
        }
    }
}